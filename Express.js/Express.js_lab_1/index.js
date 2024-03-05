const express = require('express')
const app = express()
const port = 3005

const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

let temp_database = [
    {
        surname: "Hal",
        room_num: 102,
        section: "Wed dev",
        pc_data: {
            cpu: "i7-7000k",
            gpu: "GTX-1660 TUF OC",
            ram: 16
        }
    },
    {
        surname: "Kevpanych",
        room_num: 104,
        section: "Data analytics",
        pc_data: {
            cpu: "i9-9900k",
            gpu: "RTX-3060",
            ram: 32
        }
    },
    {
        surname: "Viktor",
        room_num: 110,
        section: "WEB DEV",
        pc_data: {
            cpu: "i9-9400f",
            gpu: "4060TI",
            ram: 64
        }
    }
]


// GET REQUESTS
app.get('/', (req, res) => { // виведе всі об'єкти
    res.end(JSON.stringify(temp_database))
})
app.get('/:id', (req, res) => { // виведе об'єкт з специфічним id
    res.end(JSON.stringify(temp_database[req.params.id - 1]))
})


// POST requests
app.post('/', (req, res) => { // створить об'єкт
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
    res.status(201).send('Створено успішно')
})
app.post('/:id', (req, res) => { // редагує об'єкт з таким id
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
    temp_database[req.params.id - 1] = editedWorker
    res.status(201).send('Відредаговано успішно')
})
//DELETE REQUESTS
app.delete('/:name', (req, res) => { // видалить об'єкт
    let result = temp_database.filter((worker) => worker.surname !== req.params.name)
    if (result.length === temp_database.length) {
        res.status(200).send('Не було видалено ні одного записа')
    } else {
        let diff = temp_database.length - result.length
        temp_database = result
        console.log(temp_database)
        res.status(200).send(`Видалено ${diff} записів`)
    }
})
app.listen(port, () => {
    console.log(`Прослуховую порт : ${port}`)
})