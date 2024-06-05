import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"api/producto";
console.log(api);
class ProductosConfig{
    getProducto(){
        return axios.get(api);
    }

    getProductoById(id){
        return axios.get(`${api}/${id}`);
    }

    createProducto(depositoId, proveedorId, marcaId, producto){
        return axios.post(`${api}/${depositoId}/${proveedorId}/${marcaId}`, producto);
    }

    updateProducto(id,producto){
        return axios.put(`${api}/${id}`, producto, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }

    deleteProducto(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new ProductosConfig();