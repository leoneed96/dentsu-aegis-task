import axios from "axios";
let axiosInstance = null;
export default{
    getInstance(){
        if(!axiosInstance){
            axiosInstance = axios.create({baseURL: "http://localhost:5000/"});
        }
        return axiosInstance;
    }
}