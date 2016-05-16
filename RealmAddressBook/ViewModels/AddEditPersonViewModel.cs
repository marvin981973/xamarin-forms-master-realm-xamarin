﻿using System;
using System.ComponentModel;
using RealmAddressBook.Services;
using RealmAddressBook.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealmAddressBook.ViewModels
{
    public class AddEditPersonViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion



        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }


        private string lastName;

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        protected IDBService DBService;

        protected Person Model = new Person();

        public ICommand SaveCommand{ get; set; }

        public AddEditPersonViewModel(IDBService dbService)
        {
            DBService = dbService;
            SaveCommand = new Command(() => DoSave());

        }

      
        public void Init(string id)
        {
            Model = id != null ? DBService.GetPersonById(id) : new Person()
            {
                ID = Guid.NewGuid().ToString()
            };
            FirstName = Model.FirstName;
            LastName = Model.LastName;

        }

        protected void DoSave()
        {
            DBService.SavePerson(Model.ID, FirstName, LastName);
        }
    }
}

