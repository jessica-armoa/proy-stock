import axios from "axios";

const api = process.env.NEXT_PUBLIC_API_URL+"api/producto";

class ProductsController {
  getProducts() {
    return axios.get(api);
  }

  getProduct(id) {
    return axios.get(`${api}${id}`);
  }

  createProduct(product) {
    return axios.post(api, product);
  }

  updateProduct(id,producto) {
    return axios.put(`${api}/${id}`, producto, {
      headers: {
          'Content-Type': 'application/json'
      }
  });
  }

  deleteProduct(id) {
    return axios.delete(`${api}${id}`);
  }
}

export default new ProductsController();
