import axios from "axios";

const api = "https://proy-stock.azurewebsites.net/api/"+"producto";
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

    updateProducto(id){
        return axios.put(`${api}/${id}`);
    }

    deleteProducto(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new ProductosConfig();
