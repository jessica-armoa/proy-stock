import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"usuario/login";

class AuthConfig {
  login(credentials) {
    return axios.post(api, credentials);
  }
}

export default new AuthConfig();
