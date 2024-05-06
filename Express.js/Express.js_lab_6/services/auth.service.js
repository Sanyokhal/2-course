let jwtSecret = 'AZOV-4308';
import jwt from 'jsonwebtoken';

export const signToken = async (user) => {
    return jwt.sign({id: user['id']}, jwtSecret, {expiresIn: 24 * 60 * 60})
}


export const verifyToken = async (token) => {
    return new Promise((resolve, reject) => {
        jwt.verify(token, jwtSecret, (err, decoded) => {
            if (err) {
                console.error("Недійсний токен:", err);
                reject(err);
            } else {
                console.log("Дані з токена:", decoded);
                resolve(decoded.id);
            }
        });
    });
}
