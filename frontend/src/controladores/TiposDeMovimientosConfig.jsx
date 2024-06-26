import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"tipo-de-movimiento";

class TiposDeMovimientoConfig{
    getTiposDeMovimiento(){
        return axios.get(api);
    }

    getTipoDeMovimientoById(id){
        return axios.get(`${api}/${id}`);
    }

}

export default new TiposDeMovimientoConfig();