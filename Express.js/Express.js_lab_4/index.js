import express from 'express'

import bodyParser from 'body-parser'
import {body, validationResult} from 'express-validator'

const app = express()
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json())
const port = 3005
import {
    createNewPc,
    createNewWorker, editWorkerById,
    getAllWorkers,
    patchComputer,
    getPcDataById,
    getPcDataList,
    getWorkerById,
    removeWorkerById,
    removeWorkerBySurname
} from './database.js'

app.use((req, res, next) => {
    console.log(`${new Date().toISOString()} - ${req.method} ${req.url}`);
    next();
});
const checkIfLoggedIn = (req, res, next) => {
    const isAuthenticated = true;
    if (isAuthenticated) {
        next();
    } else {
        res.status(401).send('Не авторизовано');
    }
};

app.get('/', checkIfLoggedIn, async (req, res) => {
    let data = await getAllWorkers()
    res.json(data)
})
app.get('/computers', checkIfLoggedIn, async (req, res) => {
    let data = await getPcDataList()
    res.json(data)
})
app.get('/:id', checkIfLoggedIn, async (req, res) => {
    let data = await getWorkerById(req.params.id)
    res.json(data)
})
app.get('/computers/:id', checkIfLoggedIn, async (req, res) => {
    let data = await getPcDataById(req.params.id)
    res.json(data)
})


app.delete('/remove/id/:id', checkIfLoggedIn, async (req, res) => {
    let data = await removeWorkerById(req.params.id)
    res.json(data)
})
app.delete('/remove/surname/:surname', checkIfLoggedIn, async (req, res) => {
    let data = await removeWorkerBySurname(req.params.surname)
    res.json(data)
})


app.post('/', [checkIfLoggedIn,
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
app.post('/computers', [checkIfLoggedIn,
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


app.patch('/:id', [checkIfLoggedIn,
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
app.patch('computers/:id', [checkIfLoggedIn,
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