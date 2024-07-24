# ColorMind
Два игрока по сети управляют перснонажем и раскрашивают каждый свой 3d объект цветными сферами. Победит первый, кто раскрасит польностью объект.
Функции:
Игрок: Передвигается, подбирает сферы, подносит их к объекту - раскраске.
Объект - раскраска: Определяет подходящую подношенную сферу и раскрашивается в её цвет.
Цветная сфера: спаунится каждые n секунд, катается по игровому полю.
Счёт: обновляется от n нераскшрашенных до 0.

Возможности: По сети раскрашивать знакомые объекты, соревнуясь.

colorMind.pptx - презентация проекта

https://youtu.be/7h9VqEbrxBA - видеодемонстрация

Assets/Scripts/ - папка со скриптами

ColorManager.cs– отвечает за хранение таблицы цветов из палитры.

ServerColors.cs – отвечает за сетевое распознавание цветов.

ServerColorsSpawner.cs - отвечает за сетевой спаун цветных сфер

ButtonManager.cs – отвечает за соединения по сети и сбор ввода сетевых параметров от пользователя.

TeamsObject.cs – вывод счета игроков на сетевые клиенты.

PaintingObject.cs – логика окрашиваемых объектов (считывание цвета сферы и окраска нужной части объекта).

ClientPlayerMove.cs – сетевое передвижение персонажа, сбор сведений об окружающих игрока сферах.

ServerPlayerMove.cs – позиционирование игрока, после появления в сети, сетевой подбор сфер

ServerPlayerSpawnPoints.cs –логика добавления очков за прогресс в игре

ClientObjectWithColorsType.cs – сетевая окраска сфер после спауна.
