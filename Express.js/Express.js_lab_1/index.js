const express = require('express')
const app = express()
const port = 3000

const fs = require("fs")
const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());


// GET REQUESTS
app.get('/', (req, res) => { // виведе всі об'єкти
    fs.readFile(__dirname + '/' + 'workers.json', 'utf-8', (err, data) => {
        var workers = JSON.parse(data);
        res.end(JSON.stringify(workers));
    })
})
app.get('/:id', (req, res) => { // виведе об'єкт з специфічним id
    fs.readFile(__dirname + '/' + 'workers.json', 'utf-8', (err, data) => {
        var workers = JSON.parse(data);
        var worker = workers[req.params.id - 1]
        res.end(JSON.stringify(worker));
    })
})


// POST requests
app.post('/', (req, res) => { // створить об'єкт
    fs.readFile(__dirname + "/workers.json", 'utf8', (err, data) => {
        let workers = JSON.parse(data);
        const newWorker = {
            "surname": req.body.surname,
            "room_num": req.body.room_num,
            "section": req.body.section,
            "pc_data": {
                "cpu": req.body.cpu,
                "gpu": req.body.gpu,
                "ram": req.body.ram
            }
        };
        console.log(newWorker);
        workers.push(newWorker);
        fs.writeFile(__dirname + '/workers.json', JSON.stringify(workers, null, 2), 'utf8', (err) => {
            if (err) {
                res.status(500).send('Виникла помилка при створенні запису')
            }
            res.status(201).send('Створено успішно')
        })
    });
})
app.post('/:id', (req, res) => { // редагує об'єкт з таким id
    fs.readFile(__dirname + "/workers.json", 'utf8', (err, data) => {
        let workers = JSON.parse(data);
        const editedWorker = {
            "surname": req.body.surname,
            "room_num": req.body.room_num,
            "section": req.body.section,
            "pc_data": {
                "cpu": req.body.cpu,
                "gpu": req.body.gpu,
                "ram": req.body.ram
            }
        };
        workers[req.params.id - 1] = editedWorker
        fs.writeFile(__dirname + '/workers.json', JSON.stringify(workers, null, 2), 'utf8', (err) => {
            if (err) {
                res.status(500).send('Виникла помилка при редагуванні запису')
            }
            res.status(201).send('Відредаговано успішно')
        })
    });
})
//DELETE REQUESTS
app.delete('/:id', (req, res) => { // видалить об'єкт
    fs.readFile(__dirname + "/workers.json", 'utf8', (err, data) => {
        let workers = JSON.parse(data)
        let new_workers = []
        for (let worker in workers) {
            if (worker !== (req.params.id - 1)) {
                new_workers.push(workers[worker])
            }
        }
        fs.writeFile(__dirname + '/workers.json', JSON.stringify(workers, null, 2), 'utf8', (err) => {
            if (err) {
                res.status(500).send('Виникла помилка при створенні запису')
            }
            res.status(200).send('Видалено')
        })
    });
})
app.listen(port, () => {
    console.log(`Прослуховую порт : ${port}`)
})