import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"reporte";

class ReportesConfig{
    getReportes(){
        return axios.get(api);
    }

    getReporteId(id){
        return axios.get(`${api}/${id}`);
    }

    postReporte(ferreteriaId, deposito){
        return axios.post(`${api}/${ferreteriaId}`, deposito);
    }

    putReporte(id, deposito) {
        console.log("en el put", deposito)
        return axios.put(`${api}/${id}`, deposito, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
    deleteReporte(id){
        return axios.delete(`${api}/${id}`);
    }
}

export default new ReportesConfig();
