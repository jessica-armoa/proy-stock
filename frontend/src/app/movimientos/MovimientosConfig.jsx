import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"movimiento";

class MovimientosConfig{
    getMovimiento(){
        return axios.get(api);
    }

    getMovimientoById(id){
        return axios.get(`${api}/${id}`);
    }

    createMovimiento(movimiento){
        return axios.post(api, movimiento);
    }

    updateMovimiento(id){
        return axios.put(`${api}/${id}`);
    }

    deleteMovimiento(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new MovimientosConfig();