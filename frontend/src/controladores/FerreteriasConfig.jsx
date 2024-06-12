import axios from "axios";

const api = "https://proy-stock.azurewebsites.net/api/"+"ferreteria";

class FerreteriasConfig{
    getFerreteria(){
        return axios.get(api);
    }

    getFerreteriaId(id){
        return axios.get(`${api}/${id}`);
    }

    psotFerreteria(ferreteria){
        return axios.post(api, ferreteria);
    }

    putFerreteria(id){
        return axios.put(`${api}/${id}`);
    }

    deleteFerrteria(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new FerreteriasConfig();