import axios from "axios";
let axiosInstance = null;
export default{
    getInstance(){
        if(!axiosInstance){
            axiosInstance = axios.create({baseURL: "https://localhost:44378/"});
        }
        return axiosInstance;
    }
}