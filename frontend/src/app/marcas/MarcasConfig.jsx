import axios from "axios";

const api = "http://localhost:5282/api/marca";

class MarcasConfig{
    getMarca(){
        return axios.get(api);
    }

    getMarcaById(id){
        return axios.get(`${api}/${id}`);
    }

    createMarca(proveedorId, marca){
        return axios.post(`${api}/${proveedorId}`, marca);
    }

    updateMarca(id){
        return axios.put(`${api}/${id}`);
    }

    deleteMarca(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new MarcasConfig();