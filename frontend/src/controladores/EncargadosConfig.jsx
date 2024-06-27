import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"usuario";

class EncargadosConfig{
    getUsuarios(){
        return axios.get(api+"/all");
    }

    getDepositoId(id){
        return axios.get(`${api}/${id}`);
    }

    postDeposito(ferreteriaId, deposito){
        return axios.post(`${api}/${ferreteriaId}`, deposito);
    }

    putDeposito(id, deposito) {
        console.log("en el put", deposito)
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

export default new EncargadosConfig();
