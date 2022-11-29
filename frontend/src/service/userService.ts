import userEvent from '@testing-library/user-event';
import axios, { AxiosResponse } from 'axios';
import { json } from 'stream/consumers';
import { instance, instanceNoJWT } from '../axios';
import { User } from '../types';

const createUser = (user :User) => instanceNoJWT.post('/User', JSON.stringify(user));
const getUser = (userId: string) => instance.get<User>('/User/' + userId);
const deleteUser = (userId: string) => instance.delete('/User/' + userId);

const login = (email: string, password: string) => instanceNoJWT.post<AxiosResponse<User>>("/AuthorizationConrtoller", {}, {
  headers:{
    Password: password,
    Email: email,
}});

const UserService = {
  createUser,
  getUser,
  deleteUser,
  login
};

export default UserService;
