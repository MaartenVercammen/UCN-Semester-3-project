import axios from 'axios';

export const instanceNoJWT = axios.create({
  baseURL: process.env.API_URL,
  headers: { 'Content-Type': 'application/problem+json' }
});

export const instance = axios.create({
  baseURL: process.env.API_URL,
  headers: {
    'Content-Type': 'application/problem+json',
    Authorization: 'Bearer ' + sessionStorage.getItem('token')
  }
});

instance.interceptors.request.use(
  (config) => {
    console.log(config.headers);
    if(config.headers){
      if(config.headers.Authorization?.toString().split(' ')[1] === 'null'){
        var tries = 0;
        while(sessionStorage.getItem('token') === null && tries < 10){
          tries++;
          console.log(tries + " " + sessionStorage.getItem('token'));
        }
        console.log('token found');
        config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('token');
      }
    }

    return config;
  },
  (error) => {
    console.log(error);
    return Promise.reject(error);
  }
)

instance.interceptors.response.use(
  (config) => {
    console.log(config);
    return config;
  },
  (error) => {
    console.log(error.response);
    if (error.response.status === 401) {
      sessionStorage.removeItem('user');
      sessionStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);
