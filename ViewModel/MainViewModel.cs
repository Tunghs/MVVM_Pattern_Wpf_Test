﻿using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_test.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _iNum;

        // Number에 대하 get, set
        public int Number
        {
            get
            { return _iNum; }
            set
            {
                _iNum = value;
                // 이건 set 부분이 호출되어 Number의 값이 바뀌게 되면 UI에게 '나 변했어' 라고 알려 달라는 뜻이다.
                // 이걸 알려주면 UI는 개발자가 특별한 작업을 하지 않아도 값을 자동으로 바뀐 값으로 변경하게 된다.
                OnPropertyChanged("Number");

                // Enable이 변경되었음을 알려주어야 한다.
                OnPropertyChanged("MinusEnable");
                OnPropertyChanged("PlusEnable");

                PageContents = string.Format("{0} 페이지를 보고 있어요.", _iNum);
            }
        }

        // 프로그램이 실행되면 1페이지 부터 시작하기 위해서 생성자를 하나 만들고 그 생성자에서 Number=1 을 설정한다.
        public MainViewModel()
        {
            Number = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged("Number"); 부분에 넣은 "Number" 라는 속성이 변했음을 UI에게 자동으로 알려준다라고 이해하면 된다.
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand minusComand;
        public ICommand MinusComaand
        {
            // get이 하는 일은 minusCommad라는 ICommand 객체를 return 해주는데 이 minusCommand의 실제 수행작업이 Minus() 라는 함수로 위임해 주는 역할을 한다.
            get { return (this.minusComand) ?? (this.minusComand = new DelegateCommand(Minus)); }
        }

        // Number에서 1을 감소시키는 Number--; 작업이다.
        private void Minus()
        {
            Number--;
        }

        // Number가 1보다 작으면 버튼 비활성화
        public bool MinusEnable
        {
            get { return Number > 1 ? true : false; }
        }

        private ICommand plusCommand;
        public ICommand PlusCommand
        {
            get { return (this.plusCommand) ?? (this.plusCommand = new DelegateCommand(Plus)); }
        }
        private void Plus()
        {
            Number++;
            
        }

        // Number가 10이 넘어가면 버튼 비활성화
        public bool PlusEnable
        {
            get { return Number < 10 ? true : false; }
        }

        private string pageContent;
        public string PageContents
        {
            get { return pageContent; }
            set
            {
                pageContent = value;
                OnPropertyChanged("PageContents");
            }
        }
    }
}
