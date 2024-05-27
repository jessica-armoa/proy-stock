import axios from "axios";

const api = "http://localhost:5282/api/proveedor"

class ProveedoresConfig{
    getProveedor(){
        return axios.get(api);
    }

    getProveedorById(id){
        return axios.get(`${api}/${id}`);
    }

    createProveedor(proveedor){
        return axios.post(api, proveedor);
    }

    updateProveedor(id){
        return axios.put(`${api}/${id}`);
    }

    deleteProveedor(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new ProveedoresConfig();