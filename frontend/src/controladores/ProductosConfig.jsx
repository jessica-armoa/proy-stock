import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"producto";

class ProductosConfig{
    getProductos(){
        return axios.get(api);
    }

    getProductoId(id){
        return axios.get(`${api}/${id}`);
    }

   
    postProducto(depositoId, proveedorId, marcaId, producto) {
        return axios.post(`${api}/${depositoId}/${proveedorId}/${marcaId}`, producto);
    }
    

    putProducto(id,producto){
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
