import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"api/usuario/login";

class AuthController {
  login(credentials) {
    return axios.post(api, credentials);
  }
}

export default new AuthController();
