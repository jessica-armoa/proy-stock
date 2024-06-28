import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL + "timbrado";

class TimbradosConfig {
    getTimbrado() {
        return axios.get(api);
    }

    getTimbradoById(id) {
        return axios.get(`${api}/${id}`);
    }

    getTimbradoActivo() {
        return axios.get(api+'/activo');
    }

    postTimbrado(timbrado) {
        return axios.post(api, timbrado);
    }


}

export default new TimbradosConfig();
