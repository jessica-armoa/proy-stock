import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"motivo-por-tipo-de-movimiento";

class MotivosPorTipoDeMovimientoConfig{
    getMotivosPorTipoDeMovimiento(){
        return axios.get(api);
    }

    getMotivoPorTipoDeMovimientoById(id){
        return axios.get(`${api}/${id}`);
    }

}

export default new MotivosPorTipoDeMovimientoConfig();