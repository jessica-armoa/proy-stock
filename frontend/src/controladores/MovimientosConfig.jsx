import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"movimiento";

class MovimientosConfig{
    getMovimiento(){
        return axios.get(api);
    }

    getMovimientoById(id){
        return axios.get(`${api}/${id}`);
    }

    postMovimiento(motivoPorTipoDeMovimientoId, depositoOrigenId, depositoDestinoId, movimiento){
        return axios.post(`${api}/${motivoPorTipoDeMovimientoId}/${depositoOrigenId}/${depositoDestinoId}`, movimiento);
    }

    putMovimiento(id){
        return axios.put(`${api}/${id}`);
    }

    deleteMovimiento(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new MovimientosConfig();