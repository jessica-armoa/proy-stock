import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL + "notas-de-remision";

class NotasDeRemisionConfig {
    getNotasDeRemision() {
        return axios.get(api);
    }

    getNotaDeRemisionId(id) {
        return axios.get(`${api}/${id}`);
    }


    postNotaDeRemision(notaDeRemision) {
        return axios.post(api, notaDeRemision);
    }

    getNotaDeRemisionSiguiente(){
        return axios.get(api, 'getSiguienteNumero')
    }


    putNotaDeRemision(id, notaDeRemision) {
        return axios.put(`${api}/${id}`, notaDeRemision, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }

}

export default new NotasDeRemisionConfig();
