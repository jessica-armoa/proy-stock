import axios from "axios";

const api = "http://localhost:5282/api/deposito";

class DepositosConfig{
    getDeposito(){
        return axios.get(api);
    }

    getDepositoById(id){
        return axios.get(`${api}/${id}`);
    }

    createDeposito(ferreteriaId, deposito){
        return axios.post(`${api}/${ferreteriaId}`, deposito);
    }

    updateDeposito(id){
        return axios.put(`${api}/${id}`);
    }

    deleteDeposito(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new DepositosConfig();