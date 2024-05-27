import axios from "axios";

const api = "http://localhost:5282/api/usuario/login";

class AuthController {
  login(credentials) {
    return axios.post(api, credentials);
  }
}

export default new AuthController();
