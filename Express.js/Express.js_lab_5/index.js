import express from 'express'

import bodyParser from 'body-parser'
import {body, validationResult} from 'express-validator'
import {
    checkUser,
    createNewPc,
    createNewWorker, createUser,
    editWorkerById,
    getAllWorkers,
    getPcDataById,
    getPcDataList,
    getUser,
    getWorkerById,
    patchComputer,
    removeWorkerById,
    removeWorkerBySurname
} from './database.js'
import createError from "http-errors";

import bcrypt from 'bcrypt';
import {authCheck} from './middlewares/auth.middleware.js'
import {signToken} from './services/auth.service.js'

const app = express()
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json())
const port = 3005

app.use((req, res, next) => {
    console.log(`${new Date().toISOString()} - ${req.method} ${req.url}`);
    next();
});

app.get('/', authCheck, async (req, res) => {
    let data = await getAllWorkers()
    res.json(data)
})
app.get('/computers', authCheck, async (req, res) => {
    let data = await getPcDataList()
    res.json(data)
})
app.get('/:id', authCheck, async (req, res) => {
    let data = await getWorkerById(req.params.id)
    res.json(data)
})
app.get('/computers/:id', authCheck, async (req, res) => {
    let data = await getPcDataById(req.params.id)
    res.json(data)
})


app.delete('/remove/id/:id', authCheck, async (req, res) => {
    let data = await removeWorkerById(req.params.id)
    res.json(data)
})
app.delete('/remove/surname/:surname', authCheck, async (req, res) => {
    let data = await removeWorkerBySurname(req.params.surname)
    res.json(data)
})


app.post('/', [authCheck,
        body('surname').notEmpty().isString(),
        body('room_num').notEmpty().isInt().custom((value) => {
            console.log(value)
            if (value < 0) {
                throw new Error('Число менше 0');
            }
        }),
        body('group_name').notEmpty().isString(),
        body('pc_id').notEmpty().isInt()],
    async (req, res) => {
        const errors = validationResult(req);
        console.log(errors.array())
        if (!errors.isEmpty()) {
            res.status(400).json({errors: errors.array()})
        } else {
            let data = await createNewWorker(req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
            res.json(data)
        }
    })
app.post('/computers', [authCheck,
        body('gpu').notEmpty().isString(),
        body('cpu').notEmpty().isString(),
        body('ram').notEmpty().isInt()],
    async (req, res) => {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            res.status(400).json({errors: errors.array()})
        }
        let data = await createNewPc(req.body.gpu, req.body.cpu, req.body.ram)
        res.json(data)
    })

// ЛОГІН
app.post('/login', [
        body('username').notEmpty().isString(),
        body('password').notEmpty().isString()],
    async (req, res) => {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            res.status(400).json({errors: errors.array()})
        }
        let check = await checkUser(req.body['username'])
        if (check) {
            throw createError.NotFound('There is no User with such username');
        }
        let user = await getUser(req.body['username']);
        const passwordCheck = await bcrypt.compare(req.body.password, user['password']);
        if (!passwordCheck) {
            throw createError.Unauthorized('Incorrect password');
        }
        const token = await signToken(user)
        res.cookie("access_token", token, {httpOnly: true})
            .status(201)
            .json({
                status: 201,
                data: {token},
            });
    })


//ЛОГАУТ
app.post('/signout', async (req, res) => {
    try {
        res.clearCookie("access_token")
            .status(200)
            .json({
                status: 200,
            });
    } catch (err) {
        console.error(err)
    }
})

app.post('/create', [body('username').notEmpty().isString(),
    body('password').notEmpty().isString(),
    body('first_name').notEmpty().isString(), body('last_name').notEmpty().isString()
], async (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
        res.status(400).json({errors: errors.array()})
    }
    let {username, password, first_name, last_name} = req.body;
    let check = await checkUser(username);
    if (!check) {
        throw createError.Unauthorized('User exists');
    }
    let crypted_pass = await bcrypt.hash(password, 10);
    let result = await createUser(username, crypted_pass, first_name, last_name);
    console.log("User created. ID = " + result)
    res.json(result);
})

app.patch('/:id', [authCheck,
        body('surname').notEmpty().isString(),
        body('room_num').notEmpty().isInt(),
        body('group_name').notEmpty().isString(),
        body('pc_id').notEmpty().isInt()],
    async (req, res) => {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            res.status(400).json({errors: errors.array()})
        }
        let data = await editWorkerById(req.params.id, req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
        res.json(data)
    })
app.patch('computers/:id', [authCheck,
        body('gpu').notEmpty().isString(),
        body('cpu').notEmpty().isString(),
        body('ram').notEmpty().isInt()],
    async (req, res) => {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            res.status(400).json({errors: errors.array()})
        }
        let data = await patchComputer(req.params.id, req.body.gpu, req.body.cpu, req.body.ram)
        res.json(data)
    })

app.listen(port, () => {
    console.log(`Прослуховую порт : ${port}`)
})