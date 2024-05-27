import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"ferreteria";

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