# AlarmClock
“AlarmClock”
Приложение будильник.
При старте синхронизирует время по сети. Использует для этого два ресурса(если основной не доступен, обращается к резервному, в случае же не доступности и второго или недоступности интернет соединения синхронизируется по времени системы).В дальнейшем время синхронизируется каждый час. Изменив значение константы Data.UPLOAD_TIME, можно повысить точность. Значение по умолчанию 3600 секунд.
Будильник имеет две панели. Первая отображает текущее время в аналоговом (циферблат и стрелочки) и цифровом варианте, а также данные о синхронизации. Вторая предназначена для установки времени будильника. Установку можно производить как перетягивая стрелки(для удобства подложка циферблата меняется в зависимости от установленного времени, чтобы было понятнее это восемь утра или вечера), так и устанавливая время цифровыми значениями.
Остановить будильник можно нажав кнопку “СТОП” на второй панели.
