from datetime import datetime
from flask import current_app
from sqlalchemy.exc import SQLAlchemyError

from app.extensions import db


class Base(db.Model):
    __abstract__ = True

    def save(self, commit=True):
        db.session.add(self)
        if commit:
            try:
                db.session.commit()
            except SQLAlchemyError as e:
                db.session.rollback()
                raise e

    def delete(self):
        try:
            db.session.delete(self)
            db.session.commit()
        except SQLAlchemyError as e:
            db.session.rollback()
            raise e


class AuditMixin(object):
    create_timestamp = db.Column(db.DateTime, nullable=False, default=datetime.utcnow)
    update_timestamp = db.Column(db.DateTime,
                                 nullable=False,
                                 default=datetime.utcnow,
                                 onupdate=datetime.utcnow)
