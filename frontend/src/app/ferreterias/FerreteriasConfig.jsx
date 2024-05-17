import axios from "axios";

const api = "http://localhost:5282/api/ferreteria";

class FerreteriasConfig{
    getFerreteria(){
        return axios.get(api);
    }

    getFerreteriaById(id){
        return axios.get(`${api}/${id}`);
    }

    createFerreteria(ferreteria){
        return axios.post(api, ferreteria);
    }

    updateFerreteria(id){
        return axios.put(`${api}/${id}`);
    }

    deleteFerrteria(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new FerreteriasConfig();