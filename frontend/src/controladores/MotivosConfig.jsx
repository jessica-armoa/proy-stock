import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"motivo";

class MotivosConfig{
    getMotivos(){
        return axios.get(api);
    }

    getMotivoById(id){
        return axios.get(`${api}/${id}`);
    }

}

export default new MotivosConfig();