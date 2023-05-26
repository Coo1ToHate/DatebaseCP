# Курсовая работа по дисциплине «Базы данных»

C# + wpf + MVVM + ADO.NET + SQLite

## БД, интерфейс и запросы

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/4201003c-d84a-42be-9e7b-0b48e948da53)

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/dc232134-3c2a-476d-a472-03bb99de68d3)

```sql
SELECT * FROM Groups
SELECT * FROM Students WHERE Group_id = @groupId
SELECT COUNT(*) FROM Students WHERE Group_id = @groupId
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/dd758aa2-4a0a-4dfc-8055-1544e258b34e)

```sql
SELECT
  t.id,
  t.LastName,
  t.FirstName,
  t.MiddleName,
  t.BirthDate,
  tt.Name as 'Title',
  group_concat(DISTINCT d.name) as 'Degree',
  group_concat(DISTINCT p.name) as 'Post'
FROM Teachers t 
INNER JOIN TeacherTitle tt
  ON t.TeachingTitle_id = tt.id
INNER JOIN (TeacherDegree td INNER JOIN Degree d ON td.Degree_id=d.id)
  ON t.id = td.Teacher_id
INNER JOIN (TeacherPost tp INNER JOIN Post p ON tp.Post_id=p.id)
  ON t.id = tp.Teacher_id
GROUP BY t.id
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/8f0a465f-8d7e-423c-8bb3-a49445a1de66)

```sql
SELECT
  d.id,
  d.Date,
  l.Name as Lesson,
  tc.Name as Type,
  d.Score,
  t.id as TeacherId,
  t.LastName || ' ' || t.FirstName AS Teacher
FROM Diary d 
INNER JOIN Lessons l
  ON d.Lesson_id = l.id
INNER JOIN TypesCertification tc
  ON d.Type_id = tc.id
INNER JOIN Teachers t
  ON d.Teacher_id = t.id
WHERE d.Student_id = @studentId
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/9284b88c-12b6-450e-8d01-017d07429586)

```sql
SELECT avg(d.Score) FROM Diary d INNER JOIN Students s ON d.Student_id = s.Id WHERE s.Group_id = @id
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/34d33619-640c-464b-8208-800b89e8273b)

```sql
SELECT avg(Score) FROM Diary WHERE Student_id = @id
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/ab7ee44e-03f8-4ae4-9bee-804b8ef79c7a)

```sql
SELECT avg(Score) FROM Diary WHERE Teacher_id = @id
```

## Дополнительные вопросы

1. Супертип - это общая характеристика некоторой группы сущностей. Он описывает общие свойства и атрибуты, которые являются общими для всех подтипов этого супертипа.<br />
Подтипы сущностей - это конкретные реализации сущностей, которые наследуют все свойства супертипа и могут также иметь собственные уникаль-ные свойства и атрибуты.<br />
Для реализации супертипа и подтипов в реляционной базе данных можно использовать одну общую таблицу для супертипа "СуперТип", а так-же отдельные таблицы для каждого подтипа "Подтип1" и "Подтип2". Табли-ца "СуперТип" будет содержать общие атрибуты, а каждая из таблиц подтипов будет содержать уникальные атрибуты и ключевой атрибут, который будет ссылаться на запись в таблице "СуперТип".<br />
Пример из сущностей «Преподаватель» и «Студент».

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/aa07fcd2-f267-431a-af5d-b145c878af3d)

2. Создайте SQL-запрос по Вашей БД, выдающий ФИО только тех преподавателей, которые поставили максимальное количество оценок «удовлетворительно» по всем закрепленным предметам.

```sql
SELECT t.LastName||' '||t.FirstName||' '||t.MiddleName as 'ФИО', count(*) as 'max'
FROM Teachers t
JOIN TeacherLesson tl ON t.id = tl.Teacher_id
JOIN Diary d ON tl.Lesson_id = d.Lesson_id AND tl.Teacher_id = d.Teacher_id
WHERE d.Score = 3
GROUP BY t.id
HAVING count(*) = (SELECT max(c)
FROM (
	SELECT count(*) c
	FROM Diary d
	WHERE d.Score = 3
	GROUP BY d.Teacher_id
))
```

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/1ef95c15-4529-4a51-8fc3-04a28f3f14b1)

![изображение](https://github.com/Coo1ToHate/DatebaseCP/assets/77828075/277bed69-b451-4d37-ad78-1b6b2e92d876)

