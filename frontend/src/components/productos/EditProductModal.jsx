"use client";
import React, { useState, useEffect } from "react";
import {
  Button,
  TextInput,
  NumberInput,
  SearchSelect,
  SearchSelectItem,
  Dialog,
  DialogPanel,
} from "@tremor/react";
import { RiCloseLine } from "@remixicon/react";
import ProductosConfig from "@/controladores/ProductosConfig";
import ProveedoresConfig from "@/controladores/ProveedoresConfig";

const EditProductModal = ({ isOpen, onClose, product, onSave }) => {
  const [formData, setFormData] = useState(null);
  const [proveedores, setProveedores] = useState([]);
  const [image, setImage] = useState(null); // Estado para la nueva imagen

  useEffect(() => {
    if (product) {
      setFormData({
        ...product,
        fk_proveedor: product.proveedorId.toString(),
        proveedorNombre: product.proveedorNombre,
      });
      setImage(product.str_ruta_imagen); // Establecer la imagen actual
    }
  }, [product]);

  useEffect(() => {
    const fetchProveedores = async () => {
      const response = await ProveedoresConfig.getProveedor();
      setProveedores(response.data);
      // Asegura que el valor inicial del proveedor sea el correcto
      if (product) {
        setFormData((f) => ({
          ...f,
          fk_proveedor: product.proveedorId.toString(),
          proveedorNombre: product.proveedorNombre,
        }));
      }
    };

    fetchProveedores();
  }, [product]);

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setImage(e.target.result); // Actualizar la imagen mostrada
        setFormData({ ...formData, str_ruta_imagen: e.target.result });
      };
      reader.readAsDataURL(file); // Leer el archivo como URL de datos
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await ProductosConfig.putProducto(product.id, formData);
      onSave(); // Refresh the products list or any needed UI updates
      onClose(); // Close the modal
    } catch (error) {
      console.error("Error al actualizar producto:", error);
    }
  };

  if (!formData) return null; // Prevent rendering if formData is not set yet

  return (
    <Dialog open={isOpen} onClose={onClose} static={true} className="z-[100]">
      <DialogPanel className="sm:max-w-4xl">
        <div className="flex justify-between items-center p-4 border-b">
          <h2 className="text-xl font-semibold">Editar Producto</h2>
          <button onClick={onClose}>
            <RiCloseLine className="h-5 w-5 shrink-0" aria-hidden={true} />
          </button>
        </div>
        <form onSubmit={handleSubmit}>
          <div>
            <input
              id="file-upload"
              type="file"
              style={{ display: "none" }}
              onChange={handleImageChange}
            />
            {image && (
              <img
                src={image}
                alt="Vista previa"
                style={{ width: "30%", marginTop: "10px" }}
              />
            )}
            <label
              htmlFor="file-upload"
              className="button text-blue-500 mx-4"
              style={{ cursor: "pointer" }}
            >
              Elegir imagen
            </label>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4 p-4">
            <div>
              <label htmlFor="str_nombre">
                Nombre<span className="text-red-500">*</span>
              </label>
              <TextInput
                id="str_nombre"
                name="str_nombre"
                value={formData.str_nombre}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label htmlFor="str_descripcion">
                Descripción<span className="text-red-500">*</span>
              </label>
              <TextInput
                id="str_descripcion"
                name="str_descripcion"
                value={formData.str_descripcion}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label htmlFor="fk_marca">
                Marca<span className="text-red-500">*</span>
              </label>
              <TextInput
                id="str_marca"
                name="str_marca"
                value={formData.marcaNombre}
                onChange={handleChange}
                required
                readOnly
              />
            </div>
            <div>
              <label htmlFor="fk_proveedor">
                Proveedor<span className="text-red-500">*</span>
              </label>
              <SearchSelect
                id="fk_proveedor"
                value={formData.fk_proveedor}
                onValueChange={(value) =>
                  setFormData({ ...formData, fk_proveedor: value.toString() })
                }
              >
                {proveedores.map((proveedor) => (
                  <SearchSelectItem
                    key={proveedor.id}
                    value={proveedor.id.toString()}
                  >
                    {proveedor.str_nombre}
                  </SearchSelectItem>
                ))}
              </SearchSelect>
            </div>
            <div>
              <label htmlFor="int_cantidad_actual">Cantidad actual</label>
              <TextInput
                id="int_cantidad_actual"
                name="int_cantidad_actual"
                value={formData.int_cantidad_actual}
                min={0}
                onChange={handleChange}
                required
                readOnly
              />
            </div>
            <div>
              <label htmlFor="dec_costo_PPP">
                Costo PPP<span className="text-red-500">*</span>
              </label>
              <TextInput
                id="dec_costo_PPP"
                name="dec_costo_PPP"
                value={formData.dec_costo_PPP}
                min={0}
                onChange={handleChange}
                required 
                readOnly
              />
            </div>
            <div>
              <label htmlFor="dec_precio_mayorista">
                Precio mayorista<span className="text-red-500">*</span>
              </label>
              <NumberInput
                id="dec_precio_mayorista"
                name="dec_precio_mayorista"
                value={formData.dec_precio_mayorista}
                min={0}
                onChange={handleChange}
                required
              />
            </div>
            <div>
              <label htmlFor="dec_precio_minorista">
                Precio minorista<span className="text-red-500">*</span>
              </label>
              <NumberInput
                id="dec_precio_minorista"
                name="dec_precio_minorista"
                value={formData.dec_precio_minorista}
                min={0}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="flex justify-end p-4 border-t">
            <Button variant="secondary" onClick={onClose} className="mr-2">
              Cancelar
            </Button>
            <Button variant="primary" type="submit">
              Guardar
            </Button>
          </div>
        </form>
      </DialogPanel>
    </Dialog>
  );
};

export default EditProductModal;