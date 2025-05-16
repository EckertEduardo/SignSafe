import { createContext, useContext, useEffect, useRef, useState } from "react";
import axios, { HttpStatusCode } from 'axios'
import { toast } from "react-toastify";


export const AppContext = createContext()

export const AppContextProvider = (props) => {
    const backendUrl = import.meta.env.VITE_BACKEND_URL || "http://localhost:5000";

    const [isLoggedin, setIsLoggedin] = useState(false)
    const [userData, setUserData] = useState(null)
    const [loading, setLoading] = useState(false);

    const api = axios.create({
        baseURL: backendUrl,
        withCredentials: true,
    });

    const requestCount = useRef(0);

    const showLoading = () => {
        requestCount.current++;
        setLoading(true);
    };

    const hideLoading = () => {
        requestCount.current--;
        if (requestCount.current <= 0) {
            setLoading(false);
            requestCount.current = 0;
        }
    };

    api.interceptors.request.use(
        (config) => {
            showLoading();
            return config;
        },
        (error) => {
            hideLoading();
            return Promise.reject(error);
        }
    );

    // Response interceptor: Hide loading after response
    api.interceptors.response.use(
        (response) => {
            hideLoading();
            return response;
        },
        (error) => {
            hideLoading();
            return Promise.reject(error);
        }
    );

    const globalExceptionFilter = (error) => {
        if (error?.response?.data?.errors != null) {
            Object.entries(error.response.data.errors).forEach(x => {
                toast.error(x[1][0])
            });
        }
        else if (error?.response?.data?.data != null) {
            toast.error(error.response.data.data)
        }
        else if (error?.response?.data?.detail != null) {
            toast.error(error.response.data.detail)
        }
        else {
            toast.error("An unexpected exception ocurred")
        }
    }

    const userIsLogged = async () => {
        try {
            const response = await api.get('/users/is-logged')

            if (response.status === HttpStatusCode.Ok) {

                if (response.data.data) {
                    setIsLoggedin(true)
                }
            }

        } catch (error) {
            console.log(error?.config?.baseURL + error.message)
        }
    }

    const userIsAdmin = (roles) => {
        return roles.split(',').map(role => role.trim()).includes('Admin');
    };

    const getLoggedUserData = async () => {
        try {
            const response = await api.get('/users/get-logged-user')

            if (response.status === HttpStatusCode.Ok) {
                setUserData(response.data.data)
            }

        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    useEffect(() => {
        userIsLogged();

    }, []);

    useEffect(() => {
        if (isLoggedin) {
            getLoggedUserData();
        }
    }, [isLoggedin]);

    //Consts to export
    const value = {
        api,
        isLoggedin, setIsLoggedin,
        userData, setUserData,
        getLoggedUserData,
        loading, showLoading, hideLoading,
        globalExceptionFilter, userIsAdmin
    }

    return (
        <AppContext.Provider value={value}>
            {props.children}
        </AppContext.Provider>
    )

}
export const useAppContext = () => useContext(AppContext);