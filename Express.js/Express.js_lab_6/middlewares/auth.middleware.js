import {verifyToken} from '../services/auth.service.js'
import createError from 'http-errors'


export async function authCheck(req, res, next) {
    try {
        if (!req.headers['x-auth']) {
            throw createError.Unauthorized('Access token is required');
        }

        req.auth = await verifyToken(req.headers['x-auth']);

        next();
    } catch (err) {
        next(err);
    }
}
