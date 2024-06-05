import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"api/deposito";

class DepositosController {
  getDepositos() {
    return axios.get(api);
  }

  getDeposito(id) {
    return axios.get(`${api}${id}`);
  }

  createDeposito(deposito) {
    return axios.post(api, deposito);
  }

  updateDeposito(id, deposito) {
    return axios.put(`${api}${id}`, deposito);
  }

  deleteProduct(id) {
    return axios.delete(`${api}/${id}`);
  }*/
}

export default new DepositosController();
