<?php


//Асоціативний масив “Проект”
// (Код, автор проекту, кошторис проекту у грн., оцінки проекту у трьох номінаціях (цілі числа від 1 до 5)).
// Запит проектів, кошторис яких не більше У грн., які у трьох номінаціях у сумі набрали не менше, ніж Х балів.
$project = [
    'code' => null,
    'author' => null,
    'price' => null,
    'score' => [
        'competion_1' => null,
        'competion_2' => null,
        'competion_3' => null,
    ]
];

$projects = [
    [
        'code' => 1,
        'author' => 'Галь Олександр Віталійович',
        'price' => 1500,
        'score' => [
            'competion_1' => 5,
            'competion_2' => 4,
            'competion_3' => 5,
        ]
    ],
    [
        'code' => 2,
        'author' => 'Глуханич Станіслав',
        'price' => 1000,
        'score' => [
            'competion_1' => 4,
            'competion_2' => 5,
            'competion_3' => 4,
        ]
    ],
    [
        'code' => 3,
        'author' => 'Кириленко Ярослав',
        'price' => 1700,
        'score' => [
            'competion_1' => 5,
            'competion_2' => 5,
            'competion_3' => 5,
        ]
    ],
    [
        'code' => 4,
        'author' => 'Дзьобак Артур',
        'price' => 1750,
        'score' => [
            'competion_1' => 5,
            'competion_2' => 5,
            'competion_3' => 5,
        ]
    ],
];
if (array_key_exists('load', $_POST)) {
    load();
}
function save()
{
    global $projects;
    file_put_contents('data.json', json_encode($projects));
}

function load()
{
    global $projects;
    $json = file_get_contents('data.json');
    $json_data = json_decode($json, true);
    $projects = $json_data;
}
load();

if (isset($_POST['code'])) {
    if ($_POST['author'] != null || $_POST['price'] != null || $_POST['score'] != null || $_POST['price'] <= 0) {
        $scores_sep = explode(",", $_POST['score']);
        if (count($scores_sep) == 3) {
            foreach ($scores_sep as $score) {
                if ($score > 5 || $score <= 0) {
                    return false;
                }
            }
            $projects[] = [
                'code' => $_POST['code'],
                'author' => $_POST['author'],
                'price' => $_POST['price'],
                'score' => [
                    'competion_1' => $scores_sep[0],
                    'competion_2' => $scores_sep[1],
                    'competion_3' => $scores_sep[2],
                ]
            ];
            save();
        }
    }
}

$projects = array_filter($projects, function ($element) {
    $return_flag = true;
    if (isset($_GET['price']) && $element['price'] >= $_GET['price']) {
        $return_flag = false;
    }
    $sum = $element['score']['competion_1'] + $element['score']['competion_2'] + $element['score']['competion_3'];
    if ($return_flag && isset($_GET['min_score']) && $sum <= $_GET['min_score']) {
        $return_flag = false;
    }
    return $return_flag;
});

include 'templates/competition_table.phtml';
include 'templates/competition_form_create.phtml';
include 'templates/buttons.phtml';