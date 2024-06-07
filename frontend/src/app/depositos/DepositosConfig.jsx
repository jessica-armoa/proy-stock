import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"deposito";

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

    updateDeposito(id, deposito) {
        return axios.put(`${api}/${id}`, deposito, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
    deleteDeposito(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new DepositosConfig();