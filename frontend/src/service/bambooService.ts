import userEvent from '@testing-library/user-event';
import axios from 'axios';
import { json } from 'stream/consumers';
import { instance } from '../axios';
import {BambooSession, Seat} from '../types';

const getBambooSession= (sessionId: string) => instance.get<BambooSession>('/BambooSession/' + sessionId);
const getBambooSessions = () => instance.get<BambooSession[]>('/BambooSession');
const createBambooSession = (bambooSession: BambooSession) => instance.post<BambooSession>('/BambooSession', JSON.stringify(bambooSession));
const deleteBambooSession = (sessionId: string) => instance.delete('/BambooSession/' + sessionId);
const joinBambooSession = (sessionId: string, seatId: string) => instance.put('/BambooSession?sessionId=' + sessionId + '&seatId=' + seatId);
const getSeatsBySessionId = (sessionId: string) => instance.get<Seat[]>('/BambooSession/' + sessionId + '/seats');

const BambooService = {
  getBambooSession,
  getBambooSessions,
  createBambooSession,
  deleteBambooSession,
  joinBambooSession,
  getSeatsBySessionId
};

export default BambooService;
