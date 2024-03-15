const express = require('express')
const app = express()
const port = 3005
const {v4: uuidv4} = require('uuid');
// видалення uid
// get uid
const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

let temp_database = [
    {
        surname: "Hal",
        id: uuidv4(),
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
        id: uuidv4(),
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
        id: uuidv4(),
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
    res.json(temp_database);
})
app.get('/:id', (req, res) => { // виведе об'єкт з специфічним id
    let result = temp_database.filter((worker) => worker.id === req.params.id)
    res.json(result)
})


// POST requests
app.post('/', (req, res) => { // створить об'єкт
    const newWorker = {
        "surname": req.body.surname,
        "id": uuidv4(),
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
        for (let i in temp_database) {
            if (temp_database[i].id === req.params.id) {
                temp_database[i].surname = req.body.surname;
                temp_database[i].room_num = req.body.room_num;
                temp_database[i].section = req.body.section;
                temp_database[i].pc_data = {
                    "cpu": req.body.cpu,
                    "gpu": req.body.gpu,
                    "ram": req.body.ram
                }
                break;
            }
        }
        res.status(201).send('Відредаговано успішно')
    }
)
//DELETE REQUESTS
app.delete('/:id', (req, res) => { // видалить об'єкт
    let result = temp_database.filter((worker) => worker.id !== req.params.id)
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