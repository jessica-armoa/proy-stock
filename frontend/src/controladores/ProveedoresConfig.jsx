import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"proveedor"

class ProveedoresConfig{
    getProveedor(){
        return axios.get(api);
    }

    getProveedorById(id){
        return axios.get(`${api}/${id}`);
    }

    postProveedor(proveedor){
        return axios.post(api, proveedor);
    }

    putProveedor(id){
        return axios.put(`${api}/${id}`);
    }

    deleteProveedor(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new ProveedoresConfig();