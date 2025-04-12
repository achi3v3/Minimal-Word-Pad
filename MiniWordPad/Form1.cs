using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
namespace MiniWordPad
{
    public partial class Form1: Form
    {
        private string currentFilePath = null;
        private int savedSelectionStart = 0;
        private int savedSelectionLength = 0;


        public Form1()
        {
            InitializeComponent();
            Text = "Amiri WordPad";
            UpdateStatus("Готов");


            foreach (var size in new float[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                fontSizeComboBox.Items.Add(size);
            }


            // Заполняем список шрифтов
            foreach (FontFamily font in FontFamily.Families)
            {
                fontComboBox.Items.Add(font.Name);
            }

            fontComboBox.SelectedItem = "Times New Roman";
            fontSizeComboBox.SelectedItem = 12f;

            this.Activated += (s, e) => {
                if (foundPositions.Count > 0)
                    HighlightFoundText(); // Переподсвечиваем при возврате на форму
            };

            boldToolStripButton.CheckOnClick = true;
            italicToolStripButton.CheckOnClick = true;
            underlineToolStripButton.CheckOnClick = true;
            alignCenterToolStripButton.CheckOnClick = true;
            alignLeftToolStripButton.CheckOnClick = true;
            alignRightToolStripButton.CheckOnClick = true;

            mainToolStrip.Visible = false;
        }
        private void fontDefaultToolStripButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true; // Разрешаем выбор цвета

            if (contentRichTextBox.SelectionFont != null)
            {
                fontDialog.Font = contentRichTextBox.SelectionFont;
                fontDialog.Color = contentRichTextBox.SelectionColor;
            }

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                contentRichTextBox.SelectionFont = fontDialog.Font;
                contentRichTextBox.SelectionColor = fontDialog.Color;
            }
        }
        private void contentRichTextBox_TextChanged(object sender, EventArgs e)
        {
            // Сбрасываем поисковые данные при изменении текста
            searchText = "";
            foundPositions.Clear();
            currentPosition = 0;
        }
        private void UpdateStatus(string message)
        {
            if (mainStatusStrip.Items.Count > 0)
                statusLabel.Text = message;
        }
        private void SaveCursorPosition()
        {
            savedSelectionStart = contentRichTextBox.SelectionStart;
            savedSelectionLength = contentRichTextBox.SelectionLength;
        }
        private void RestoreCursorPosition()
        {
            contentRichTextBox.SelectionStart = savedSelectionStart;
            contentRichTextBox.SelectionLength = savedSelectionLength;
            contentRichTextBox.ScrollToCaret();
        }


        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fontComboBox.SelectedItem != null && contentRichTextBox.SelectionFont != null)
            {
                SaveCursorPosition();

                string fontName = fontComboBox.SelectedItem.ToString();
                float fontSize = contentRichTextBox.SelectionFont.Size;
                FontStyle fontStyle = contentRichTextBox.SelectionFont.Style;

                contentRichTextBox.SelectionFont = new Font(fontName, fontSize, fontStyle);
                UpdateStatus($"Шрифт изменен на: {fontName}");

                contentRichTextBox.Focus();
                RestoreCursorPosition();
            }
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null && contentRichTextBox.SelectionFont != null)
            {
                SaveCursorPosition();


                float newSize = float.Parse(fontSizeComboBox.SelectedItem.ToString());
                string fontName = contentRichTextBox.SelectionFont.FontFamily.Name;
                FontStyle fontStyle = contentRichTextBox.SelectionFont.Style;

                contentRichTextBox.SelectionFont = new Font(fontName, newSize, fontStyle);
                UpdateStatus($"Размер шрифта изменен на: {newSize}");

                contentRichTextBox.Focus();
                RestoreCursorPosition();
            }
        }

        // ===== Функции меню =====
        private void NewFile()
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения?", "Amiri WordPad",
                                          MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            contentRichTextBox.Clear();
            currentFilePath = null;
            Text = "Amiri WordPad - Новый документ";
        }
        private void OpenFile()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (openDialog.FileName.EndsWith(".rtf"))
                            contentRichTextBox.LoadFile(openDialog.FileName);
                        else
                            contentRichTextBox.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);

                        currentFilePath = openDialog.FileName;
                        Text = $"Amiri WordPad - {Path.GetFileName(currentFilePath)}";
                        UpdateStatus("Файл открыт");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs();
            else
            {
                try
                {
                    if (currentFilePath.EndsWith(".rtf"))
                        contentRichTextBox.SaveFile(currentFilePath);
                    else
                        contentRichTextBox.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    UpdateStatus("Файл сохранен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }

        private void SaveFileAs()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|RTF файлы (*.rtf)|*.rtf";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveDialog.FileName;
                    SaveFile();
                    Text = $"Amiri WordPad - {Path.GetFileName(currentFilePath)}";
                }
            }
        }

        // ===== Обработчики меню =====
        private void newToolStripMenuItem_Click(object sender, EventArgs e) => NewFile();
        private void openToolStripMenuItem_Click(object sender, EventArgs e) => OpenFile();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile();
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) => SaveFileAs();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Simple WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    return;
            }
            Application.Exit();
        }

        // ===== Панель инструментов =====
        private void fontToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void colorToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void boldToolStripButton_Click(object sender, EventArgs e)
        {
            //ApplyFontStyle(FontStyle.Bold);
            Set_BIU_Style(FontStyle.Bold);
        }

        private void italicToolStripButton_Click(object sender, EventArgs e)
        {
            //ApplyFontStyle(FontStyle.Italic);
            Set_BIU_Style(FontStyle.Italic);
        }

        private void underlineToolStripButton_Click(object sender, EventArgs e)
        {
            //ApplyFontStyle(FontStyle.Underline);
            Set_BIU_Style(FontStyle.Underline);
        }
        //private void ApplyFontStyle(FontStyle style)
        //{
        //    if (contentRichTextBox.SelectionLength > 0)
        //    {
        //        SaveCursorPosition();

        //        int start = contentRichTextBox.SelectionStart;
        //        int length = contentRichTextBox.SelectionLength;

        //        // Применяем стиль ко всему выделению
        //        for (int i = 0; i < length; i++)
        //        {
        //            contentRichTextBox.SelectionStart = start + i;
        //            contentRichTextBox.SelectionLength = 1;

        //            Font currentFont = contentRichTextBox.SelectionFont ?? contentRichTextBox.Font;
        //            FontStyle newStyle = currentFont.Style ^ style; // Переключаем стиль

        //            contentRichTextBox.SelectionFont = new Font(
        //                currentFont.FontFamily,
        //                currentFont.Size,
        //                newStyle);
        //        }

        //        RestoreCursorPosition();
        //        contentRichTextBox.Focus();
        //    }
        //}
        //private void ApplyFontStyle(FontStyle style)
        //{
        //    if (contentRichTextBox.SelectionLength > 0)
        //    {
        //        // Для выделенного текста
        //        int start = contentRichTextBox.SelectionStart;
        //        int length = contentRichTextBox.SelectionLength;

        //        for (int i = 0; i < length; i++)
        //        {
        //            contentRichTextBox.SelectionStart = start + i;
        //            contentRichTextBox.SelectionLength = 1;

        //            Font currentFont = contentRichTextBox.SelectionFont ?? contentRichTextBox.Font;
        //            FontStyle newStyle = currentFont.Style ^ style; // Переключаем стиль

        //            contentRichTextBox.SelectionFont = new Font(
        //                currentFont.FontFamily,
        //                currentFont.Size,
        //                newStyle);
        //        }

        //        // Восстанавливаем выделение
        //        contentRichTextBox.SelectionStart = start;
        //        contentRichTextBox.SelectionLength = length;
        //    }
        //    else
        //    {
        //        // Для нового текста
        //        Font currentFont = contentRichTextBox.SelectionFont ?? contentRichTextBox.Font;
        //        FontStyle newStyle = currentFont.Style ^ style;

        //        contentRichTextBox.SelectionFont = new Font(
        //            currentFont.FontFamily,
        //            currentFont.Size,
        //            newStyle);
        //    }

        //    contentRichTextBox.Focus();

        //    // Обновляем состояние кнопок
        //    Font font = contentRichTextBox.SelectionFont ?? contentRichTextBox.Font;
        //    boldToolStripButton.Checked = font.Bold;
        //    italicToolStripButton.Checked = font.Italic;
        //    underlineToolStripButton.Checked = font.Underline;
        //}
        private void alignLeftToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            alignRightToolStripButton.Checked = false;
            alignCenterToolStripButton.Checked = false;

            UpdateStatus("Выравнивание по левому краю");

        }

        private void alignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            alignRightToolStripButton.Checked = false;
            alignLeftToolStripButton.Checked = false;


            UpdateStatus("Выравнивание по центру");
        }

        private void alignRightToolStripButton_Click(object sender, EventArgs e)
        {
            contentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            alignCenterToolStripButton.Checked = false;
            alignLeftToolStripButton.Checked = false;


            UpdateStatus("Выравнивание по правому краю");
        }

        private string currentSearchText = "";
        private int currentSearchIndex = 0;
        private List<int> searchOccurrences = new List<int>();

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (contentRichTextBox.SelectionLength > 0)
            {
                try
                {
                    DataObject data = new DataObject();

                    // Добавляем и обычный текст, и RTF
                    data.SetText(contentRichTextBox.SelectedText); // Простой текст
                    if (contentRichTextBox.SelectedRtf != null)
                    {
                        data.SetData(DataFormats.Rtf, contentRichTextBox.SelectedRtf); // Форматирование
                    }

                    Clipboard.Clear();
                    Clipboard.SetDataObject(data, true);
                    UpdateStatus("Текст скопирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выделите текст для копирования", "Инфо",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Вставка (исправленная версия)
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, есть ли текст в буфере
                if (Clipboard.ContainsText() || Clipboard.ContainsText(TextDataFormat.Rtf))
                {
                    // Если есть RTF-форматирование - вставляем с форматированием
                    if (Clipboard.ContainsText(TextDataFormat.Rtf))
                    {
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Rtf));
                    }
                    // Иначе вставляем как обычный текст
                    else
                    {
                        contentRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
                    }
                    UpdateStatus("Текст вставлен");
                }
                else
                {
                    MessageBox.Show("Буфер обмена не содержит текста", "Инфо",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вставки: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string searchText = "";
        private int currentPosition = 0;
        private List<int> foundPositions = new List<int>();
        private Form searchForm;

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelSearch.Width < 100)
            {
                panelSearch.Width = 150;
                panelReplace.Width = 1;
            }
            else
            {
                panelSearch.Width = 1;
            }
        }
        private void ReplaceToolStripButton_Click(object sender, EventArgs e)
        {
            if (panelReplace.Width < 100)
            {
                panelReplace.Width = 150;
                panelSearch.Width = 1;
            }
            else
            {
                panelReplace.Width = 1;
            }
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            FindNext(searchBox.Text);
        }
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            FindPrevious(searchBox.Text);
        }
        private void buttonNextReplace_Click(object sender, EventArgs e)
        {
            FindNext(textBoxWhat.Text);
        }
        private void buttonPrevReplace_Click(object sender, EventArgs e)
        {
            FindPrevious(textBoxWhat.Text);
        }
        private void FindNext(string text)
        {
            if (text != searchText)
            {
                // Если текст поиска изменился, ищем заново
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

            currentPosition++;
            if (currentPosition >= foundPositions.Count)
            {
                currentPosition = 0;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();
        }

        private void FindPrevious(string text)
        {
            if (text != searchText)
            {
                searchText = text;
                currentPosition = 0;
                FindAllOccurrences();
            }

            if (foundPositions.Count == 0)
            {
                MessageBox.Show("Текст не найден!");
                return;
            }

            currentPosition -= 1;
            if (currentPosition < 0)
            {
                currentPosition = foundPositions.Count - 1;
            }

            HighlightFoundText();
            contentRichTextBox.Focus();

        }

        private void FindAllOccurrences()
        {
            foundPositions.Clear();
            int index = 0;

            while (index < contentRichTextBox.Text.Length)
            {
                index = contentRichTextBox.Find(searchText, index, RichTextBoxFinds.None);
                if (index == -1) break;

                foundPositions.Add(index);
                index += searchText.Length;
            }
        }

        private void HighlightFoundText()
        {
            // Сбрасываем выделение (если нужно)
            contentRichTextBox.SelectionLength = 0;

            if (foundPositions.Count > 0 && currentPosition < foundPositions.Count)
            {
                // Устанавливаем начало и длину выделения (оно будет подсвечиваться стандартным образом)
                contentRichTextBox.SelectionStart = foundPositions[currentPosition];
                contentRichTextBox.SelectionLength = searchText.Length;

                // Прокручиваем RichTextBox к выделxенному тексту
                contentRichTextBox.ScrollToCaret();

                // Refresh не обязателен, но можно оставить, если нужно
                contentRichTextBox.Refresh();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Redo();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            contentRichTextBox.Undo();
        }

        private void Set_BIU_Style(FontStyle style)
        {
            if (contentRichTextBox.SelectionFont != null)
            {
                System.Drawing.Font currentFont = contentRichTextBox.SelectionFont;
                FontStyle newStyle;
                if (currentFont.Style.HasFlag(style))
                {
                    newStyle = currentFont.Style & ~style;
                }
                else
                {
                    newStyle = currentFont.Style | style;
                }
                contentRichTextBox.SelectionFont = new Font(currentFont, newStyle);
            }
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            ReplaceCurrent();
        }
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }

        //private void ReplaceCurrent()
        //{
        //    if (string.IsNullOrEmpty(textBoxWhat.Text)) return;

        //    if (textBoxWhat.Text != searchText || foundPositions.Count == 0)
        //    {
        //        searchText = textBoxWhat.Text;
        //        currentPosition = 0;
        //        FindAllOccurrences();
        //        if (foundPositions.Count == 0) return;
        //    }

        //    // Заменяем только если выделен искомый текст
        //    if (contentRichTextBox.SelectedText == searchText)
        //    {
        //        contentRichTextBox.SelectedText = textBoxOn.Text;
        //        FindAllOccurrences(); // Перестраиваем позиции после замены
        //    }

        //    FindNext(searchText); // Переходим к следующему
        //}
        private void ReplaceCurrent()
        {
            if (string.IsNullOrEmpty(textBoxWhat.Text)) return;

            // Инициализация поиска при первом вызове или изменении текста
            if (textBoxWhat.Text != searchText || foundPositions.Count == 0)
            {
                searchText = textBoxWhat.Text;
                FindAllOccurrences();
                currentPosition = foundPositions.Count > 0 ? 0 : -1;
            }

            if (foundPositions.Count == 0) return;

            // Заменяем текущее выделение, если оно совпадает
            if (contentRichTextBox.SelectedText == searchText)
            {
                contentRichTextBox.SelectedText = textBoxOn.Text;

                // После замены находимся на той же позиции, но текст изменился
                // Просто переходим к следующему без увеличения позиции
                FindAllOccurrences();
                if (foundPositions.Count == 0) return;

                // Корректируем позицию, если текущая стала невалидной
                if (currentPosition >= foundPositions.Count)
                    currentPosition = 0;
            }
            else
            {
                // Если не было замены, переходим к следующему
                currentPosition++;
            }

            // Циклический переход
            if (currentPosition >= foundPositions.Count)
                currentPosition = 0;

            HighlightFoundText();
            contentRichTextBox.Focus();
        }
        private void ReplaceAll()
        {
            if (!string.IsNullOrEmpty(textBoxWhat.Text))
            {
                ReplaceAllOccurrences(textBoxWhat.Text, textBoxOn.Text);
            }
        }
        //private void buttonReplace_Click(object sender, EventArgs e)
        //{
        //    string searchText = textBoxWhat.Text;
        //    string replaceText = textBoxOn.Text;

        //    if (string.IsNullOrEmpty(searchText)) return;

        //    // Если текст для поиска изменился - ищем все вхождения заново
        //    if (searchText != this.searchText)
        //    {
        //        this.searchText = searchText;
        //        currentPosition = 0;
        //        FindAllOccurrences();
        //    }

        //    // Если нет совпадений - выходим
        //    if (foundPositions.Count == 0)
        //    {
        //        MessageBox.Show("Текст не найден!");
        //        return;
        //    }

        //    // Заменяем текущее выделение
        //    if (contentRichTextBox.SelectedText == searchText)
        //    {
        //        contentRichTextBox.SelectedText = replaceText;

        //        // Обновляем позиции после замены
        //        FindAllOccurrences();
        //    }

        //    // Переходим к следующему совпадению
        //    FindNext(searchText);
        //}

        //private void buttonReplaceAll_Click(object sender, EventArgs e)
        //{
        //    string searchText = textBoxWhat.Text;
        //    string replaceText = textBoxOn.Text;

        //    if (string.IsNullOrEmpty(searchText)) return;

        //    ReplaceAllOccurrences(searchText, replaceText);
        //}

        private void ReplaceAllOccurrences(string searchText, string replaceText)
        {
            if (string.IsNullOrEmpty(searchText)) return;

            contentRichTextBox.SuspendLayout();
            int replaceCount = 0;
            int index = 0;

            while (index < contentRichTextBox.TextLength)
            {
                index = contentRichTextBox.Find(searchText, index, RichTextBoxFinds.None);
                if (index == -1) break;

                contentRichTextBox.SelectionStart = index;
                contentRichTextBox.SelectionLength = searchText.Length;
                contentRichTextBox.SelectedText = replaceText;

                replaceCount++;
                index += replaceText.Length;
            }

            contentRichTextBox.ResumeLayout();
            UpdateStatus($"Заменено {replaceCount} вхождений");
        }
        private void contentRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            //if (contentRichTextBox.SelectionFont != null)
            //{
            //    boldToolStripButton.Checked = contentRichTextBox.SelectionFont.Bold;
            //    italicToolStripButton.Checked = contentRichTextBox.SelectionFont.Italic;
            //    underlineToolStripButton.Checked = contentRichTextBox.SelectionFont.Underline;

            //    fontComboBox.SelectedItem = contentRichTextBox.SelectionFont.FontFamily.Name;
            //    fontSizeComboBox.SelectedItem = contentRichTextBox.SelectionFont.Size;
            //}

            //switch (contentRichTextBox.SelectionAlignment)
            //{
            //    case HorizontalAlignment.Left:
            //        alignLeftToolStripButton.Checked = true;
            //        alignCenterToolStripButton.Checked = false;
            //        alignRightToolStripButton.Checked = false;
            //        break;
            //    case HorizontalAlignment.Center:
            //        alignLeftToolStripButton.Checked = false;
            //        alignCenterToolStripButton.Checked = true;
            //        alignRightToolStripButton.Checked = false;
            //        break;
            //    case HorizontalAlignment.Right:
            //        alignLeftToolStripButton.Checked = false;
            //        alignCenterToolStripButton.Checked = false;
            //        alignRightToolStripButton.Checked = true;
            //        break;
            //}
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (contentRichTextBox.TextLength > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?", "Amiri WordPad",
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}