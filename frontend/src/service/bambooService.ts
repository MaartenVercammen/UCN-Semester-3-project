import userEvent from '@testing-library/user-event';
import axios from 'axios';
import { json } from 'stream/consumers';
import { instance } from '../axios';
import {BambooSession} from '../types';

const getBambooSession= (sessionId: string) => instance.get<BambooSession>('/BambooSession/' + sessionId);
const getBambooSessions = () => instance.get<BambooSession[]>('/BambooSession');
const createBambooSession = (bambooSession: BambooSession) => instance.post('/BambooSession', JSON.stringify(bambooSession));
const BambooService = {

  getBambooSession,
  getBambooSessions,
  createBambooSession,
};

export default BambooService;
