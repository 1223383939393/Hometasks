
let tasks = JSON.parse(localStorage.getItem('tasks')) || [];
let currentFilter = 'all';

// Хватаем элементы со страницы
const form = document.getElementById('task-form');
const list = document.getElementById('task-list');
const filters = document.querySelectorAll('.filter-btn');
const sortSelect = document.getElementById('sort-select');

// Слушатели событий
form.addEventListener('submit', handleTaskSubmit);
sortSelect.addEventListener('change', renderTasks);

filters.forEach(btn => {
    btn.addEventListener('click', (e) => {
        filters.forEach(f => f.classList.remove('active'));
        e.target.classList.add('active');
        currentFilter = e.target.dataset.filter;
        renderTasks();
    });
});

// Главная функция добавления / сохранения изменений
function handleTaskSubmit(e) {
    e.preventDefault(); // Чтобы страница не перезагружалась

    const idInput = document.getElementById('task-id').value;
    const title = document.getElementById('task-title').value;
    const desc = document.getElementById('task-desc').value;
    const priority = document.getElementById('task-priority').value;
    const date = document.getElementById('task-date').value;

    if (idInput) {
        // Редактируем старую
        const index = tasks.findIndex(t => t.id == idInput);
        tasks[index] = { ...tasks[index], title, desc, priority, date };
        document.getElementById('submit-btn').textContent = 'Добавить задачу';
    } else {
        // Создаем новую
        const newTask = {
            id: Date.now(), // Уникальный ID
            title, desc, priority, date,
            completed: false
        };
        tasks.push(newTask);
    }

    form.reset();
    document.getElementById('task-id').value = '';
    saveData();
    renderTasks();
}

// Функция отрисовки задач
function renderTasks() {
    list.innerHTML = '';
    let filteredTasks = [...tasks];

    // Применяем фильтры
    if (currentFilter === 'active') filteredTasks = filteredTasks.filter(t => !t.completed);
    if (currentFilter === 'completed') filteredTasks = filteredTasks.filter(t => t.completed);
    if (currentFilter === 'high') filteredTasks = filteredTasks.filter(t => t.priority === 'high');

    // Применяем сортировку
    const sortValue = sortSelect.value;
    if (sortValue === 'date') {
        filteredTasks.sort((a, b) => new Date(a.date) - new Date(b.date));
    } else if (sortValue === 'priority') {
        const pLevel = { high: 3, medium: 2, low: 1 };
        filteredTasks.sort((a, b) => pLevel[b.priority] - pLevel[a.priority]);
    }

    // Выводим на экран
    filteredTasks.forEach(task => {
        const priorityRu = { high: 'Высокий', medium: 'Средний', low: 'Низкий' };
        
        const div = document.createElement('div');
        div.className = `task-item ${task.completed ? 'completed' : ''}`;
        div.innerHTML = `
            <div class="task-info">
                <div class="task-title">${task.title}</div>
                <div class="task-meta">
                    Дедлайн: <b>${task.date}</b> | 
                    Приоритет: <span class="priority-${task.priority}">${priorityRu[task.priority]}</span>
                </div>
                ${task.desc ? `<div class="task-desc">${task.desc}</div>` : ''}
            </div>
            <div class="task-actions">
                <button class="btn-done" onclick="toggleTask(${task.id})">✔</button>
                <button class="btn-edit" onclick="editTask(${task.id})">✎</button>
                <button class="btn-delete" onclick="deleteTask(${task.id})">✖</button>
            </div>
        `;
        list.appendChild(div);
    });

    updateStats();
}

// Удаление
function deleteTask(id) {
    tasks = tasks.filter(t => t.id !== id);
    saveData();
    renderTasks();
}

// Смена статуса (выполнено/не выполнено)
function toggleTask(id) {
    const task = tasks.find(t => t.id === id);
    task.completed = !task.completed;
    saveData();
    renderTasks();
}

// Закидываем данные обратно в форму для редактирования
function editTask(id) {
    const task = tasks.find(t => t.id === id);
    document.getElementById('task-id').value = task.id;
    document.getElementById('task-title').value = task.title;
    document.getElementById('task-desc').value = task.desc;
    document.getElementById('task-priority').value = task.priority;
    document.getElementById('task-date').value = task.date;
    
    document.getElementById('submit-btn').textContent = 'Сохранить изменения';
    window.scrollTo(0, 0); // Кидаем юзера наверх к форме
}

// Обновление цифр в статистике
function updateStats() {
    const total = tasks.length;
    const completed = tasks.filter(t => t.completed).length;
    
    document.getElementById('total-tasks').textContent = total;
    document.getElementById('active-tasks').textContent = total - completed;
    document.getElementById('completed-tasks').textContent = completed;
}

// Сохранение в память браузера
function saveData() {
    localStorage.setItem('tasks', JSON.stringify(tasks));
}

// Первый запуск отрисовки при загрузке страницы
renderTasks();