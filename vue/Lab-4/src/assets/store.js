import { createStore } from 'vuex'

//5. Відділ кадрів. База даних про співробітників фірми: паспортні дані, education, speciality,
// position, revenue. Організувати вибір за довільним запитом.
const store = createStore({
    state () {
        return {
            employees:[
                {
                    "passport": {
                        "name": "Іван",
                        "surname": "Петренко",
                        "series": "АВ123456",
                        "address": "вул. Головна, 123"
                    },
                    "education": "Вища",
                    "speciality": "Інформаційні технології",
                    "position": "Програміст",
                    "revenue": 50000
                },
                {
                    "passport": {
                        "name": "Марія",
                        "surname": "Сидоренко",
                        "series": "BC654321",
                        "address": "вул. Сонячна, 45"
                    },
                    "education": "Вища",
                    "speciality": "Фінанси",
                    "position": "Фінансовий аналітик",
                    "revenue": 60000
                },
                {
                    "passport": {
                        "name": "Олександр",
                        "surname": "Коваленко",
                        "series": "DE789012",
                        "address": "вул. Проспектна, 67"
                    },
                    "education": "Вища",
                    "speciality": "Менеджмент",
                    "position": "Менеджер з розвитку",
                    "revenue": 55000
                },
                {
                    "passport": {
                        "name": "Ольга",
                        "surname": "Григоренко",
                        "series": "FG345678",
                        "address": "вул. Лісова, 89"
                    },
                    "education": "Вища",
                    "speciality": "Маркетинг",
                    "position": "Менеджер з маркетингу",
                    "revenue": 58000
                },
                {
                    "passport": {
                        "name": "Андрій",
                        "surname": "Іванов",
                        "series": "GH901234",
                        "address": "вул. Нова, 12"
                    },
                    "education": "Вища",
                    "speciality": "ІТ-консалтинг",
                    "position": "Консультант з ІТ",
                    "revenue": 52000
                },
                {
                    "passport": {
                        "name": "Тетяна",
                        "surname": "Павленко",
                        "series": "IJ567890",
                        "address": "вул. Західна, 34"
                    },
                    "education": "Вища",
                    "speciality": "Логістика",
                    "position": "Логіст",
                    "revenue": 54000
                },
                {
                    "passport": {
                        "name": "Євген",
                        "surname": "Мельник",
                        "series": "KL123456",
                        "address": "вул. Південна, 78"
                    },
                    "education": "Вища",
                    "speciality": "Інженерія",
                    "position": "Інженер-конструктор",
                    "revenue": 62000
                },
            ]
        }
    },
    mutations: {
        increment (state) {
            state.count++
        }
    }
})

export default store;