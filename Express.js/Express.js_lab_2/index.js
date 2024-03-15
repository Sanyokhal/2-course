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
    getPcDataById,
    getPcDataList,
    getWorkerById,
    removeWorkerById,
    removeWorkerBySurname
} from './database.js'

app.get('/', async (req, res) => {
    let data = await getAllWorkers()
    res.json(data)
}) //+
app.get('/:id', async (req, res) => {
    let data = await getWorkerById(req.params.id)
    res.json(data)
})//+
app.get('/computers', async (req, res) => {
    let data = await getPcDataList()
    res.json(data)
}) // - Чомусб не виводиться список
app.get('/computers/:id', async (req, res) => {
    let data = await getPcDataById(req.params.id)
    res.json(data)
}) // +
app.delete('/remove/id/:id', async (req, res) => {
    let data = await removeWorkerById(req.params.id)
    res.json(data)
}) // + Вертає кількість видалених рядків
app.delete('/remove/surname/:surname', async (req, res) => {
    let data = await removeWorkerBySurname(req.params.surname)
    res.json(data)
}) // + Вертає кількість видалених рядків
app.post('/', async (req, res) => {
    let data = await createNewWorker(req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
    res.json(data)
}) // +
app.post('/:id', async (req, res) => {
    let data = await editWorkerById(req.params.id, req.body.surname, req.body.room_num, req.body.group_name, req.body.pc_id)
    res.json(data)
})
app.post('/computers', async (req, res) => {
    let data = await createNewPc(req.body.gpu, req.body.cpu, req.body.ram)
    res.json(data)
})
app.listen(port, () => {
    console.log(`Прослуховую порт : ${port}`)
})