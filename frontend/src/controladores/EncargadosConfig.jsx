import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"usuario";

class EncargadosConfig{
    getUsuarios(){
        return axios.get(api+"/all");
    }


    postEncargado(encargado){
        return axios.post(api+"/register-encargado", encargado);
    }
}

export default new EncargadosConfig();
