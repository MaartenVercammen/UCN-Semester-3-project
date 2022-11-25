import userEvent from '@testing-library/user-event';
import axios from 'axios';
import { json } from 'stream/consumers';
import { instance } from '../axios';
import { User } from '../types';

const createUser = (user :User) => instance.post('/User', JSON.stringify(user));
const getUser = (userId: string) => instance.get<User>('/User/' + userId);

const UserService = {
  createUser,
  getUser
};

export default UserService;
