#ifndef OPTIONS_H
#define OPTIONS_H

#include <QDialog>
#include <QMainWindow>
#include <QFontComboBox>

namespace Ui {

}

QT_BEGIN_NAMESPACE
namespace Ui {  class options; }
namespace Mw { class MainWindow; }
QT_END_NAMESPACE

class options : public QDialog
{
    Q_OBJECT

public:
    explicit options(QWidget *parent = nullptr);
    ~options();
    void setValues(Mw::MainWindow *mw);
private:
    Mw::MainWindow *mw;
    Ui::options *ui;
    QFontComboBox *font;
};

#endif // OPTIONS_H
