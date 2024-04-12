import express from 'express'

import bodyParser from 'body-parser'

const app = express()
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json())
const port = 3005
import {
    createNewPc,
    createNewWorker, editWorkerById,
    getAllWorkers,
    getLogs,
    logRequest,
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

const checkIfLoggedIn = async (req, res, next) => {
    const isAuthenticated = true;
    if (isAuthenticated) {
        await logRequest(req.method, req.url)
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
app.get('/logs', checkIfLoggedIn, async (req, res) => {
    let data = await getLogs()
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


app.post('/', checkIfLoggedIn, async (req, res) => {
    let data = await createNewWorker(req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
    res.json(data)
})
app.post('/computers', checkIfLoggedIn, async (req, res) => {
    let data = await createNewPc(req.body.gpu, req.body.cpu, req.body.ram)
    res.json(data)
})


app.patch('/:id', checkIfLoggedIn, async (req, res) => {
    let data = await editWorkerById(req.params.id, req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
    res.json(data)
})
app.patch('/computers/:id', checkIfLoggedIn, async (req, res) => {
    let data = await patchComputer(req.params.id, req.body.gpu, req.body.cpu, req.body.ram)
    res.json(data)
})


app.listen(port, () => {
    console.log(`Прослуховую порт : ${port}`)
})