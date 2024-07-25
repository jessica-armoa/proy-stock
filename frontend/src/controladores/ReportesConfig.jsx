import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"reporte";

class ReportesConfig{
    getMasVendidos(){
        return axios.get(api+'/top5productosMasVendidos');
    }

    getMenosVendidos(id){
        return axios.get(api+'/top5productosMenosVendidos');
    }

    getPerdidasPorProducto(){
        return axios.get(api+'/perdidasPorProducto');
    }

    getPerdidasPorDeposito(){
        return axios.get(api+'/perdidasPorDeposito');
    }

    getStockCritico() {
        return axios.get(api+'/productosConCantidadMinima')
    }
    
}

export default new ReportesConfig();
