import axios from "axios";

const api = "http://localhost:5282/api/depositos";

class DepositosConfig{
    getDeposito(){
        return axios.get(api);
    }

    getDepositoById(id){
        return axios.get(`${api}/${id}`);
    }

    createDeposito(deposito){
        return axios.post(api, deposito);
    }

    updateDeposito(id){
        return axios.put(`${api}/${id}`);
    }

    deleteDeposito(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new DepositosConfig();