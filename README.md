# Курсовая работа по дисциплине «Базы данных»

C# + wpf + MVVM + ADO.NET + SQLite

## БД, интерфейс и запросы

![изображение](/assets/Screenshot_7.jpg)

![изображение](/assets/Screenshot_1.jpg)

```sql
SELECT * FROM Groups
SELECT * FROM Students WHERE Group_id = @groupId
SELECT COUNT(*) FROM Students WHERE Group_id = @groupId
```

![изображение](/assets/Screenshot_2.jpg)

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

![изображение](/assets/Screenshot_3.jpg)

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

![изображение](/assets/Screenshot_4.jpg)

```sql
SELECT avg(d.Score) FROM Diary d INNER JOIN Students s ON d.Student_id = s.Id WHERE s.Group_id = @id
```

![изображение](/assets/Screenshot_5.jpg)

```sql
SELECT avg(Score) FROM Diary WHERE Student_id = @id
```

![изображение](/assets/Screenshot_6.jpg)

```sql
SELECT avg(Score) FROM Diary WHERE Teacher_id = @id
```

## Дополнительные вопросы

1. Супертип - это общая характеристика некоторой группы сущностей. Он описывает общие свойства и атрибуты, которые являются общими для всех подтипов этого супертипа.<br />
Подтипы сущностей - это конкретные реализации сущностей, которые наследуют все свойства супертипа и могут также иметь собственные уникаль-ные свойства и атрибуты.<br />
Для реализации супертипа и подтипов в реляционной базе данных можно использовать одну общую таблицу для супертипа "СуперТип", а так-же отдельные таблицы для каждого подтипа "Подтип1" и "Подтип2". Табли-ца "СуперТип" будет содержать общие атрибуты, а каждая из таблиц подтипов будет содержать уникальные атрибуты и ключевой атрибут, который будет ссылаться на запись в таблице "СуперТип".<br />
Пример из сущностей «Преподаватель» и «Студент».

![изображение](/assets/Screenshot_8.jpg)

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

![изображение](/assets/Screenshot_9.jpg)

![изображение](/assets/Screenshot_10.jpg)

