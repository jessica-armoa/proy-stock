import axios from "axios";

const api = "http://localhost:5282/api/producto";

class UsersConfig {
  getProducts() {
    return axios.get(api);
  }



}

export default new UsersConfig();